% Create a device and open communication with the device.
cparInitialize;
IDs = cparList;
dev = cparGetDevice(IDs(1));

try
    % Create the pressure waveforms
    waveform01 = cparCreateWaveform(1, 1);
    cparWaveform_Step(waveform01, 20, 0);
    cparWaveform_Inc(waveform01, 30, 1);
    cparWaveform_Dec(waveform01, 20, 1);
   
    waveform02 = cparCreateWaveform(2, 1);    
    cparSetWaveform(dev, waveform01, waveform02);

    % Start the stimulation
    cparStart(dev, 'bp', true);
    
    data = cparInitializeSampling;
    % Wait until stimulation has completed
    while (cparIsRunning(dev))
        fprintf('State: %s\n', dev.State.ToString()); 
        pause(1);
        data = cparGetData(dev, data);
    end
    fprintf('State: %s\n', dev.State.ToString()); 
    data = cparFinalizeSampling(dev, data);

catch me
    me
end
