% Cuff Pressure Algometry Reseach (CPAR) Toolbox
%
% Initialization and management of CPAR devices
%   cparInitialize         - Initialize the Instrument database.
%   cparList               - List all installed CPAR devices.
%   cparGetDevice          - Get a CPAR device from the Instrument database.
%
% Creation and generation of pressure stimuli
%   cparCreateWaveform     - Create an empty pressure waveform
%   cparWaveform_Step      - Add a step in pressure to a waveform
%   cparWaveform_Inc       - Add a linear increase in pressure to a waveform
%   cparWaveform_Dec       - Add a linear decrease in pressure to a waveform
%   cparSetWaveform        - Add a pressure waveform to a cpar device.
%   cparStart              - Start a presssure stimulation
%   cparStop               - Stop a pressure stimulation before it is completed
%
% Data retrieval and handling
%   cparInitializeSampling - Initialize a sampling structure
%   cparGetData            - Collect data into a sampling structure
%   cparFinalizeSampling   - Finalize a sampling structure
%   cparStartSampling      - Start sampling 
%   cparStopSampling       - Stop sampling
%   cparPlot               - Plot the results in a sampling structure
%
% Device state and handling
%   cparStatus             - Get the status of a cpar device.
%   cparError              - Get the error description (if any) of a cpar device
%   cparIsRunning          - Check if a pressure stimulation is running
%   cparPing               - Check the connection to a cpar device

