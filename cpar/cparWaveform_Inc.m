function cparWaveform_Increment(func, dp, t)
% cparWaveform_Inc Add a increment instruction to a waveform
%   cparWaveform_Inc(func, dp, t)
    func.Instructions.Add(LabBench.Interface.Algometry.Instruction.Increment(dp, t));
end

