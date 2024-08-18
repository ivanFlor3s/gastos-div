import { AppStateModel } from '@core/state';

export const GetCurrentUserId = () => {
    const app: AppStateModel = JSON.parse(localStorage.getItem('app') || '');
    return app.user?.id;
};
