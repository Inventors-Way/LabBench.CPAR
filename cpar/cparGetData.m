function [data_out] = cparGetData(dev, data)
% cparGetData
%   [dataOut] = cparGetData(dev, dataIn)
    samples = dev.GetUpdates();

    for n = 0:samples.Count-1
        data.Pressure01.Add(samples.Item(n).ActualPressure.Item(0));
        data.Pressure02.Add(samples.Item(n).ActualPressure.Item(1));
        data.Target01.Add(samples.Item(n).TargetPressure.Item(0));
        data.Target02.Add(samples.Item(n).TargetPressure.Item(1));
        data.VAS.Add(samples.Item(n).VasScore);
        data.Final01 = samples.Item(n).FinalPressure.Item(0);
        data.Final02 = samples.Item(n).FinalPressure.Item(1);
        data.FinalVAS = samples.Item(n).FinalVasScore;
    end
    data_out = data;
end