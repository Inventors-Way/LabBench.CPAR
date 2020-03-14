% Create a device and open communication with the device.
dev = cparCreate('COM18');
cparOpen(dev);

% Create a stimulus that can be used for temporal summation
pon = cparPulse(50, 2, 0);
poff = cparPulse(0, 4, 2);
combined = cparCombined();
cparCombinedAdd(combined, pon);
cparCombinedAdd(combined, poff);
stimulus = cparCreateStimulus(1, 5, combined);

% Update the device with the created stimulus
cparSetStimulus(dev, stimulus);

% Make sure the other channel is set to zero.
cparSetStimulus(dev, cparCreateStimulus(2, 1, cparPulse(0, 0.1, 0))); 

% Start the stimulation
cparStart(dev, 'b', true);

% Wait until stimulation has completed
pause(1);
while (dev.State == LabBench.Interface.AlgometerState.STATE_STIMULATING)
    fprintf('State: %s\n', dev.State.ToString()); 
    pause(1);
end
fprintf('State: %s\n', dev.State.ToString()); 

% Retrive data and plot it.
data = cparGetData(dev);
cparPlot(data);
cparClose(dev);

