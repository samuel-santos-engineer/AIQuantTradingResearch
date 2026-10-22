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
    volume_y: tuple | None = None
    command_epoch: int = 0
    acknowledged: int = -1
    request_epoch: int = 0
    in_flight: bool = False
    pending: bool = False

    def command(self, rebuild=False):
        self.command_epoch += 1
        if rebuild:
            self.mount += 1

    def request(self):
        if self.in_flight:
            self.pending = True
            return None
        self.request_epoch += 1
        self.in_flight = True
        return self.request_epoch

    def finish(self, epoch):
        if not self.in_flight or epoch != self.request_epoch:
            return False
        self.in_flight = False
        if self.pending:
            self.pending = False
            self.request()
        return True

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
        self.volume_y = None
        self.command()
