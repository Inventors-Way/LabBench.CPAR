function cparSetStimulus(dev, stimulus)
% cparSetStimulus Set the stimulus for a pressure channel
%   cparSetStimulus(channel, stimulus)
channel = dev.driver.Channels.Item(stimulus.channel);
channel.SetStimulus(stimulus.repeat, stimulus.waveform);