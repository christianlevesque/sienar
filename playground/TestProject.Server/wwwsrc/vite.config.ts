import { defineConfig } from 'vite';

export default defineConfig({
	// appType: 'mpa',
	build: {
		outDir: '../wwwroot'
	},
	server: {
		origin: 'http://localhost:5173'
	},
	css: {
		preprocessorOptions: {
			scss: {
				silenceDeprecations: [
					'import',
					'mixed-decls',
					'color-functions',
					'global-builtin'
				]
			}
		}
	}
});
