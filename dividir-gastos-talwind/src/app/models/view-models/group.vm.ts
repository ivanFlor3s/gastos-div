import { SpentItem } from '../dtos';
import { GroupMemberVM } from './group-member';

export interface GroupVM {
    id: number;
    name: string;
    description: string;
    createdAt: string;
    imageUrl: string;
    isAdmin: boolean;
    users: GroupMemberVM[];
    spents: SpentItem[];
    totalSpent: number;
}
