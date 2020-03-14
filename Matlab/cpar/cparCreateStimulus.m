function [stimulus] = cparCreateStimulus(channel, repeat, period, waveform)
% cparCreateStimulus Create a stimulus from a waveform
%    [stimulus] = cparCreateStimulus(channel, repeat, period, waveform)
stimulus.channel = channel;
stimulus.repeat = repeat;
stimulus.period = period;
stimulus.waveform = waveform;