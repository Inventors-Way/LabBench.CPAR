function [waveform] = cparCreateWaveform(channel, repeat)
% cparCreateWaveformProgram Create a waveform program
%   [waveform] = cparCreateWaveformProgram(channel, repeat)
waveform = LabBench.Instruments.CPAR.Functions.SetWaveformProgram;
waveform.Channel = channel - 1;
waveform.Repeat = repeat;
