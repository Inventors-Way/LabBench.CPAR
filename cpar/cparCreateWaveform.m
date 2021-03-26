function [waveform] = cparCreateWaveform(channel, repeat)
% cparCreateWaveformProgram Create a waveform program
%   [waveform] = cparCreateWaveformProgram(channel, repeat) this function
%   creates an empty waveform for pressure outlet no. [channel] that will
%   be repeated [repeat] number of times.
%
% Note:
%
%
% See also, cparWaveform_Step, cparWaveform_Inc, cparWaveform_Dec
waveform = LabBench.Instruments.CPAR.Functions.SetWaveformProgram;
waveform.Channel = channel - 1;
waveform.Repeat = repeat;
