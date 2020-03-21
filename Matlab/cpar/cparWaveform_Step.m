function cparWaveform_Step(func, p, t)
% cparWaveform_Step Add a step instruction to a waveform
%   cparWaveform_Step(func, p, t)
    func.Instructions.Add(LabBench.CPAR.Instruction.Step(p, t));
end