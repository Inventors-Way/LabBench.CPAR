function cparSetWaveform(dev, channel01, channel02)
% cparSetWaveform Add a pressure waveform to a cpar device.

dev.Execute(channel01);
dev.Execute(channel02);