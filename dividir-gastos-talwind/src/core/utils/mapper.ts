import { NgbDate } from '@ng-bootstrap/ng-bootstrap';

export class Mapper {
    public static mapDate(date?: NgbDate): Date {
        if (!date) {
            throw new Error('Date is required');
        }
        return new Date(date.year, date.month - 1, date.day);
    }
    public static mapNgbDate(date: Date): NgbDate {
        console.log(date);
        if (!date) {
            throw new Error('Date is required');
        }
        if (typeof date == 'string') {
            date = new Date(date);
        }
        return new NgbDate(
            date.getFullYear(),
            date.getMonth() + 1,
            date.getDate()
        );
    }
}
