using System.Runtime.Versioning;
using NAudio.Wave;

[assembly: SupportedOSPlatform("windows")]

namespace Tui;

public sealed class MusicPlayer : IDisposable {
    private WaveOut? _output;
    private LoopStream? _loop;

    public float Volume {
        get => _output?.Volume ?? 0f;
        set => _output?.Volume = Math.Clamp(value, 0f, 1f);
    }

    /// <summary>Plays a file (relative to the app base directory) on a loop.</summary>
    public void Play(string relativePath) {
        Stop();

        string path = Path.Combine(AppContext.BaseDirectory, relativePath);
        if (!File.Exists(path)) {
            throw new FileNotFoundException(path);
        }

        _loop = new LoopStream(new AudioFileReader(path));
        _output = new WaveOut();
        _output.Init(_loop);
        _output.Volume = 0.5f;
        _output.Play();
    }

    private void Stop() {
        _output?.Stop();
        _output?.Dispose();
        _output = null;
        _loop?.Dispose();
        _loop = null;
    }

    public void Dispose() => Stop();
}

/// <summary>Wraps a WaveStream and rewinds to the start when it ends, so playback loops.</summary>
internal sealed class LoopStream(WaveStream source) : WaveStream {
    public override WaveFormat WaveFormat => source.WaveFormat;

    public override long Length => source.Length;

    public override long Position {
        get => source.Position;
        set => source.Position = value;
    }

    public override int Read(byte[] buffer, int offset, int count) {
        int total = 0;
        while (total < count) {
            int read = source.Read(buffer, offset + total, count - total);
            if (read == 0) {
                if (total > 0) {
                    break; // hand back what we have first, then restart
                }

                source.Position = 0;
            }

            total += read;
        }

        return total;
    }

    protected override void Dispose(bool disposing) {
        if (disposing) {
            source.Dispose();
        }

        base.Dispose(disposing);
    }
}