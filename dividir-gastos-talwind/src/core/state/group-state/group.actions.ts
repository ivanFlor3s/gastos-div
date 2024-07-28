import { CreateGroupRequest } from '@app/interfaces';
import { GroupVM } from '@app/models/view-models';
import { GettingGroupError } from './group.state';
import { AddSpentDto, SpentItem } from '@app/models/dtos';

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

export class StartAddSpent {
    static readonly type = '[Groups] StartAddSpent';
    constructor(public body: AddSpentDto) {}
}

export class DeleteSpent {
    static readonly type = '[Groups] DeleteSpent';
    constructor(public spentId: number) {}
}

export class GetSpent {
    static readonly type = '[Groups] GetSpent';
    constructor(public spentId: number) {}
}

export class SetEditingSpent {
    static readonly type = '[Groups] SetEditingSpent';
    constructor(public spent: SpentItem | null) {}
}
