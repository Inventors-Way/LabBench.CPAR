function [stimulus] = cparCreateStimulus(channel, repeat, waveform)
% cparCreateStimulus Create a stimulus from a waveform
%    [stimulus] = cparCreateStimulus(channel, repeat, waveform)
stimulus.channel = channel;
stimulus.repeat = repeat;
stimulus.waveform = waveform;