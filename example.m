% Create a device and open communication with the device.
cparInitialize;
dev = cparGetDevice('CPAR:1');

try
    % Create a stimulus that can be used for temporal summation
    waveform = cparCreateWaveform(1, 1);
    cparWaveform_Step(waveform, 10, 1);
    cparWaveform_Step(waveform, 20, 1);
    cparWaveform_Step(waveform, 30, 1);
    cparWaveform_Step(waveform, 40, 1);
    cparWaveform_Step(waveform, 50, 1);
    dev.Execute(waveform);
   
    waveform = cparCreateWaveform(2, 1);
    dev.Execute(waveform);

    % Start the stimulation
    cparStart(dev, 'bp', true);

    % Wait until stimulation has completed
    pause(1);
    while (dev.State == LabBench.Interface.Algometry.AlgometerState.STATE_STIMULATING)
        fprintf('State: %s\n', dev.State.ToString()); 
        pause(1);
    end
    fprintf('State: %s\n', dev.State.ToString()); 

catch me
    me
end
