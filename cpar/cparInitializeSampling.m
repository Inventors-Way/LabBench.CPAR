function data = cparInitializeSampling(expectedTime)
%UNTITLED3 Summary of this function goes here
%   Detailed explanation goes here
    if ~exist('expectedTime', 'file')
        expectedTime = 1;
    end

    data.Pressure01 = NET.createGeneric('System.Collections.Generic.List',{'System.Double'}, expectedTime * 20);
    data.Pressure02 = NET.createGeneric('System.Collections.Generic.List',{'System.Double'}, expectedTime * 20);
    data.Target01 = NET.createGeneric('System.Collections.Generic.List',{'System.Double'}, expectedTime * 20);
    data.Target02 = NET.createGeneric('System.Collections.Generic.List',{'System.Double'}, expectedTime * 20);
    data.VAS = NET.createGeneric('System.Collections.Generic.List',{'System.Double'}, expectedTime * 20);
    data.Final01 = 0;
    data.Final02 = 0;
    data.FinalVAS = 0;
end

