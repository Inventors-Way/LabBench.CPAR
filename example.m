% Create a device and open communication with the device.
%
% If the script is called multiple times, then this will produce a warning
% that the instrument database is allready initialized. This has no bad
% consequences.
cparInitialize;

% Next step is to retrieve the cpar device. We do this by assuming that
% there is only one cpar device installed on the system, by retrieving all
% the IDs of cpar devices from LabBench, and the getting the first device
% on the list.
%
% If there is more than one cpar device on the machine this code needs to
% be rewritten and the device ID must be known and inserted into the
% script.
IDs = cparList;
dev = cparGetDevice(IDs(1));

try
    % Create the pressure waveforms one for each pressure outlet 1 and 2.
    %
    % An empty waveform is first created with the cparCreateWaveform
    % function, which as argument takes which pressure outlet to use (1 or
    % 2) and how many times the waveform shall be repeated.
    %
    % Afterwards the waveform is populated with instructions that are used
    % by the waveform interpreter in the cpar device to generate the
    % pressure waveform. There are three instructions; step, dec, and inc.
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
