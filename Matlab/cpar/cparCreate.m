function [dev] = cparCreate(port)
% Create a CPAR driver
%    [CPAR] = cparCreate(port)

dev.DriverPath = fullfile(fileparts(mfilename('fullpath')),'libs', 'LabBench.CPAR.dll');   
dev.Assembly = NET.addAssembly(dev.DriverPath);

dev.driver = LabBench.CPAR.CPARDevice;
dev.driver.Location = Inventors.ECP.Location.Parse(port);