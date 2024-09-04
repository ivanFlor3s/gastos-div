import { DateTime } from 'luxon';

export class Helper {
    static isACurrentDate(date: string): boolean {
        const now = DateTime.now().toISODate() as string;
        const dateToCompare = DateTime.fromISO(date).toISODate() as string;

        return now > dateToCompare;
    }

    static generateRandomPassword(length: number): string {
        const specialChars = '!@#$%^&*()_+~`|}{[]:;?><,./-=';
        const numbers = '0123456789';
        const uppercaseLetters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ';
        const lowercaseLetters = 'abcdefghijklmnopqrstuvwxyz';

        // Garantizar que haya al menos uno de cada tipo de carácter
        const randomSpecialChar =
            specialChars[Math.floor(Math.random() * specialChars.length)];
        const randomNumber =
            numbers[Math.floor(Math.random() * numbers.length)];
        const randomUppercase =
            uppercaseLetters[
                Math.floor(Math.random() * uppercaseLetters.length)
            ];
        const randomLowercase =
            lowercaseLetters[
                Math.floor(Math.random() * lowercaseLetters.length)
            ];

        // Inicializar la contraseña con un carácter de cada tipo
        let password =
            randomSpecialChar +
            randomNumber +
            randomUppercase +
            randomLowercase;

        // Rellenar la contraseña con caracteres aleatorios hasta alcanzar la longitud deseada
        const allChars =
            specialChars + numbers + uppercaseLetters + lowercaseLetters;
        for (let i = password.length; i < length; i++) {
            const randomChar =
                allChars[Math.floor(Math.random() * allChars.length)];
            password += randomChar;
        }

        // Mezclar los caracteres para evitar que los primeros sean siempre los mismos
        return password
            .split('')
            .sort(() => 0.5 - Math.random())
            .join('');
    }
}
