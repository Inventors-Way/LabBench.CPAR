function [db] = cparInitialize()
% Initialize the LabBench instrument database.
%
% See also, cparGetDevice
config = cparLibraryInfo;
AddLibrary(config, 'LabBench.Instruments.dll')
AddLibrary(config, 'LabBench.Instruments.CPAR.dll')

try
    db = LabBench.Instruments.InstrumentDB.Create();    
catch exception    
    fprintf("Warning: %s\n", exception.ExceptionObject.Message)
end

function AddLibrary(config, library)
    DriverPath = fullfile(config.library_path, library);   
    NET.addAssembly(DriverPath);
