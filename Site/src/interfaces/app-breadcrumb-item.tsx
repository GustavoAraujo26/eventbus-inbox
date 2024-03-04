import { ReactNode } from "react";

export default interface AppBreadcrumbItem {
    id: number,
    icon: ReactNode,
    text: string,
    goTo: string,
    isPage: boolean
}