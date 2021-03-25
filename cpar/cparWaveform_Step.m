function cparWaveform_Step(waveform, p, t)
% cparWaveform_Step Add a step instruction to a waveform
%   cparWaveform_Step(func, p, t)
    waveform.Instructions.Add(LabBench.Interface.Algometry.Instruction.Step(p, t));
end