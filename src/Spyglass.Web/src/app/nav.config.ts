import { NavItem } from './layout/navItem';

export let navigationConfig: NavItem[] = [
    { name: 'Home', route: '', icon: 'home' },
    { name: 'Dashboard', route: '/dashboard', icon: 'dashboard' },
    { name: 'Metrics', route: '/metrics', icon: 'assessment' },
    { name: 'Credentials', route: '/credentials', icon: 'lock outline' }
];
