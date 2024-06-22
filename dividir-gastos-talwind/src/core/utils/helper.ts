import { DateTime } from 'luxon';

export class Helper {
    static isACurrentDate(date: string): boolean {
        const now = DateTime.now().toISODate() as string;
        const dateToCompare = DateTime.fromISO(date).toISODate() as string;

        return now > dateToCompare;
    }
}
