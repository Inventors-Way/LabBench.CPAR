function [IDs] = cparList()
% cparList List installed CPAR devices on the system
%   [IDs] = cparList()

    devices = LabBench.Instruments.InstrumentDB.Instruments;
    IDs = [];
    
    for n = 0:devices.Count - 1
        record = devices.Item(n);
       
        if record.EquipmentType == LabBench.Interface.InstrumentType.CPAR            
            IDs = [IDs; sprintf("%s", record.ID)]; %#ok<AGROW> It is acceptable as it will always be a very short list
        end
    end
end