import { UserVM } from '@app/models/view-models';

export interface SpentItem {
    id: number;
    groupId: number;
    amount: number;
    description: string;
    createdAt: Date;
    spentMode: number;
    authorId: string;
    author: UserVM;
    payedAt: Date;
    participants: UserVM[];
}
