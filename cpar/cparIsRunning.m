function running = cparIsRunning(dev)
%UNTITLED Summary of this function goes here
%   Detailed explanation goes here
   running = dev.State == LabBench.Interface.Algometry.AlgometerState.STATE_STIMULATING;
end

