function cparGetData(dev, data)
% cparGetData
%   [dataOut] = cparGetData(dev, dataIn)
    samples = dev.GetUpdates();

    for n = 0:samples.Count-1
        data.P01.Add(samples.Item(n).ActualPressure.Item(0));
        data.P02.Add(samples.Item(n).ActualPressure.Item(1));
    end
end