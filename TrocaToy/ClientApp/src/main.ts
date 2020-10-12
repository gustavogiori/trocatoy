import './polyfills.ts';

import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { environment } from './environments/environment';
import { AppModule } from './app/';

if (environment.production) {

}

platformBrowserDynamic().bootstrapModule(AppModule);
