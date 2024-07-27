import { NgbDate } from '@ng-bootstrap/ng-bootstrap';

export class Mapper {
    public static mapDate(date?: NgbDate): Date {
        if (!date) {
            throw new Error('Date is required');
        }
        return new Date(date.year, date.month - 1, date.day);
    }
}
