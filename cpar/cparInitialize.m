function [db] = cparInitialize()
% Initialize the LabBench instrument database.
%
% See also, cparGetDevice
config = cparLibraryInfo;
AddLibrary(config, 'LabBench.Instruments.dll')
AddLibrary(config, 'LabBench.Instruments.CPAR.dll')

db = LabBench.Instruments.InstrumentDB.Create();

function AddLibrary(config, library)
    DriverPath = fullfile(config.library_path, library);   
    NET.addAssembly(DriverPath);
