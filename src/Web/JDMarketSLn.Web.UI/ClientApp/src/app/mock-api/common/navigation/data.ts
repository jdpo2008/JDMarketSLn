/* tslint:disable:max-line-length */
import { FuseNavigationItem } from '@fuse/components/navigation';

export const defaultNavigation: FuseNavigationItem[] = [
    {
        id: 'pages',
        title: 'Paginas',
        type: 'group',
        //icon: 'heroicons_outline:chart-pie',
        //link: '/dashboard',
    },
    {
        id: 'dashboard',
        title: 'Dashboard',
        type: 'basic',
        icon: 'heroicons_outline:chart-pie',
        link: '/dashboard',
    },
    {
        id: 'seguridad',
        title: 'Seguridad',
        type: 'group',
        //icon: 'heroicons_outline:chart-pie',
        //link: '/dashboard',
    },
    {
        id: 'usuarios',
        title: 'Users',
        type: 'basic',
        icon: 'heroicons_outline:users',
        link: '/seguridad/users',
    },
    {
        id: 'roles',
        title: 'Roles',
        type: 'basic',
        icon: 'heroicons_outline:key',
        link: '/seguridad/roles',
    },
    {
        id: 'mantenimiento',
        title: 'Mantenimiento',
        type: 'group',
        //icon: 'heroicons_outline:chart-pie',
        //link: '/dashboard',
    },
    {
        id: 'products',
        title: 'Products',
        type: 'basic',
        icon: 'heroicons_outline:beaker',
        link: '/mantenimiento/products',
    },
    {
        id: 'categories',
        title: 'Categories',
        type: 'basic',
        icon: 'heroicons_outline:adjustments',
        link: '/mantenimiento/categories',
    },
    {
        id: 'supliers',
        title: 'Supliers',
        type: 'basic',
        icon: 'heroicons_outline:newspaper',
        link: '/mantenimiento/supliers',
    },
];
export const compactNavigation: FuseNavigationItem[] = [
    {
        id: 'dashboard',
        title: 'Dashboard',
        type: 'basic',
        icon: 'heroicons_outline:chart-pie',
        link: '/dashboard',
    },
];
export const futuristicNavigation: FuseNavigationItem[] = [
    {
        id: 'example',
        title: 'Example',
        type: 'basic',
        icon: 'heroicons_outline:chart-pie',
        link: '/example',
    },
];
export const horizontalNavigation: FuseNavigationItem[] = [
    {
        id: 'example',
        title: 'Example',
        type: 'basic',
        icon: 'heroicons_outline:chart-pie',
        link: '/example',
    },
];
