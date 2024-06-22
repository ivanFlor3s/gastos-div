// eslint-disable-next-line @typescript-eslint/no-var-requires
const Dotenv = require('dotenv-webpack');

module.exports = {
    // plugins: [new EnvironmentPlugin(['API_URL'])],
    plugins: [
        new Dotenv({
            systemvars: true,
        }),
    ],
};
