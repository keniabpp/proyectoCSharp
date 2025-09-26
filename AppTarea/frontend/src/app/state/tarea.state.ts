

import { signal, WritableSignal } from '@angular/core';


export const nuevaAsignacion: WritableSignal<boolean> = signal(false);
export const nuevaAsignacionTitulo: WritableSignal<string | null> = signal(null);
