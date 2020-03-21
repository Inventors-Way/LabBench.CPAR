function cparWaveform_Dec(func, dp, t)
% cparWaveform_Dev Add a decrement instruction to a waveform
%   cparWaveform_Dev(func, dp, t)
    func.Instructions.Add(LabBench.CPAR.Instruction.Decrement(dp, t));
end

