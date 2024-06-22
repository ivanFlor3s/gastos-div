import { GroupVM } from '@app/models/view-models';

export interface GroupDetail {
    group: GroupVM | null;
    spents: any[];
    liquidaciones: any[];
}
