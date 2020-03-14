function [err] = cparError(dev)
% cparError Retrive error information 
%   [err] = cparError(dev)
err = dev.driver.Error;