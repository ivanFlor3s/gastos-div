import { UserVM } from './user.vm';

export interface BasicGroupVM {
    id: number;
    name: string;
    description: string;
    users: UserVM[];
}
