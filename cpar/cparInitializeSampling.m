function data = cparInitializeSampling(expectedTime)
%UNTITLED3 Summary of this function goes here
%   Detailed explanation goes here
    if ~exist('expectedTime', 'file')
        expectedTime = 1;
    end

    data.P01 = NET.createGeneric('System.Collections.Generic.List',{'System.Double'}, expectedTime * 20);
    data.P02 = NET.createGeneric('System.Collections.Generic.List',{'System.Double'}, expectedTime * 20);

end

