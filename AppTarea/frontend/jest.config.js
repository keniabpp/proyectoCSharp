module.exports = {
  preset: 'jest-preset-angular',
  setupFilesAfterEnv: ['<rootDir>/setup-jest.ts'],
  testEnvironment: 'jsdom',
  transform: {
    '^.+\\.(ts|mjs|js)$': 'jest-preset-angular',
    '^.+\\.html$': '<rootDir>/htmlTransform.js',
  },
  moduleFileExtensions: ['ts', 'html', 'js', 'json'],
  collectCoverage: true,
  coverageReporters: ['html'],
  testPathIgnorePatterns: ['<rootDir>/dist/'], // 👈 Ignora dist
};
