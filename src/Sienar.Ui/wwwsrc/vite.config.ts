import { defineConfig } from 'vite';

export default defineConfig({
	// appType: 'mpa',
	build: {
		outDir: '../wwwroot'
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
