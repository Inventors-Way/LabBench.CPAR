function cparStart(dev, condition, forced)
% cparStart
%   cparStart(dev, condition)
if strcmp(condition, 'v')
    stop = LabBench.Interface.AlgometerStopCriterion.STOP_CRITERION_ON_BUTTON_VAS;
elseif (strcmp(condition, 'vb'))
    stop = LabBench.Interface.AlgometerStopCriterion.STOP_CRITERION_ON_BUTTON;        
else
   error('Invalid stop condition, valid values are v or vb'); 
end
        
dev.driver.Start(stop, forced)
