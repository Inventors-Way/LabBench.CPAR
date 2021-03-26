function [IDs] = cparList()
% cparList List installed CPAR devices on the system

    devices = LabBench.Instruments.InstrumentDB.Instruments;
    IDs = [];
    
    for n = 0:devices.Count - 1
        record = devices.Item(n);
       
        if record.EquipmentType == LabBench.Interface.InstrumentType.CPAR            
            str = sprintf("%s", record.ID);
            IDs = [IDs; str];
        end
    end
end