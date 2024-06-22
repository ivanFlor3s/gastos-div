import { UserVM } from './user.vm';

export interface GroupVM {
    id: number;
    name: string;
    description: string;
    createdAt: string;
    imageUrl: string;
    users: UserVM[];
}
