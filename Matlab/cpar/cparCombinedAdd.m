function cparCombinedAdd(combined, waveform)
% cparCombinedAdd Add a waveform to a combined waveform
%   [out] = cparCombinedAdd(combined, waveform)
list = combined.StimulusList;
list.Add(waveform);