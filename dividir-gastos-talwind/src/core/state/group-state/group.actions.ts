import { CreateGroupRequest } from '@app/interfaces';
import { GroupVM } from '@app/models/view-models';
import { GettingGroupError } from './group.state';

export class StartGettingGroups {
    static readonly type = '[Groups] StartGettingGroups';
    constructor(public name: string) {}
}

export class StartCreatingGroups {
    static readonly type = '[Groups] StartCreatigGroups';
    constructor(public body: CreateGroupRequest) {}
}

export class AddGroup {
    static readonly type = '[Groups] AddGroup';
    constructor(public group: GroupVM) {}
}

export class StartGettingGroup {
    static readonly type = '[Groups] StartGettingGroup';
    constructor(public groupId: number) {}
}

export class SetErrorInGroupDetail {
    static readonly type = '[Groups] SetErrorInGroupDetail';
    constructor(public error: GettingGroupError) {}
}
