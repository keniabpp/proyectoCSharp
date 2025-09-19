

import { signal, WritableSignal } from '@angular/core';

// Notificación de nueva tarea creada
export const nuevaAsignacion: WritableSignal<boolean> = signal(false);
export const nuevaAsignacionTitulo: WritableSignal<string | null> = signal(null);
