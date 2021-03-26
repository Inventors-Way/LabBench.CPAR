function [ready] = cparIsReady(dev)
% cparIsReady Is the device ready to perform a pressure stimulation

if dev.Ready
    ready = 1;
else
    ready = 0;
end