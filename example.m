% Create a device and open communication with the device.
cparInitialize;
dev = cparGetDevice('CPAR:1');

try
    % Create a stimulus 
    waveform = cparCreateWaveform(1, 1);
    cparWaveform_Step(waveform, 20, 0);
    cparWaveform_Inc(waveform, 30, 1);
    cparWaveform_Dec(waveform, 20, 1);
    cparWaveform_Step(waveform, 0, 1);
    cparWaveform_Step(waveform, 30, 1);
    cparWaveform_Step(waveform, 0, 0.5);
    dev.Execute(waveform);
   
    waveform = cparCreateWaveform(2, 1);
    dev.Execute(waveform);

    % Start the stimulation
    cparStart(dev, 'bp', true);
    data = cparInitializeSampling;
    % Wait until stimulation has completed
    while (cparIsRunning(dev))
        fprintf('State: %s\n', dev.State.ToString()); 
        pause(1);
        cparGetData(dev, data);
    end
    fprintf('State: %s\n', dev.State.ToString()); 
    data = cparFinalizeSampling(dev, data);

catch me
    me
end
