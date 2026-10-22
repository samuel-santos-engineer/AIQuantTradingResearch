"""Offline state feasibility only. Does not capture native browser relayout."""
from dataclasses import dataclass


@dataclass
class Viewport:
    x: tuple | None = None
    y: tuple | None = None
    revision: int = 0
    mount: int = 1
    live: bool = True
    expired: bool = False

    def manual(self, x, y, revision, mount):
        if mount != self.mount or revision <= self.revision:
            return False
        if len(x) != 2 or x[0] >= x[1] or len(y) != 2 or y[0] >= y[1]:
            raise ValueError("invalid viewport")
        self.x, self.y, self.revision, self.live = tuple(x), tuple(y), revision, False
        return True

    def reconcile(self, first, last, rebuild=False):
        if first > last:
            raise ValueError("invalid history")
        if rebuild:
            self.mount += 1
        self.expired = bool(self.x and (self.x[1] < first or self.x[0] > last))
        # Preserve absolute ranges even on partial overlap; expiry requires explicit user disposition.
        return self.x, self.y

    def return_live(self):
        self.x, self.y, self.live, self.expired = None, None, True, False
        self.revision += 1
