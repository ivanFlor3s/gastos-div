import { SpentMode } from '@app/models/enums/spent-mode.enum';
import { NameValue } from '@app/models/view-models';

export interface AddSpentDto {
    amount: number;
    description: string;
    participants: NameValue<string>[];
    authorId: string;
    how: SpentMode;
    payedAt: Date;
}
