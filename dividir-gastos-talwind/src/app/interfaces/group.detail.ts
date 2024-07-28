import { SpentItem } from '@app/models/dtos';
import { GroupVM } from '@app/models/view-models';

export interface GroupDetail {
    group: GroupVM | null;
    spents: SpentItem[];
}
