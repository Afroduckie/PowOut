# PowOut
A basic EXiLED 3.0 plugin for SCP: Secret Lab that adds configurable rolling blackouts.

Default CONFIG file. The door unlocking currently is unimplemented.
```
pow_out:
# Is 'LowPower' plugin enabled?
  is_enabled: true
  # CASSIE plays warning on PowOut events? [Default: True]
  pow_out_cassie: true
  # CASSIE voice line to play at PowOut.
  pow_out_msg: Error .g1 .g2 .g4 malfunction .g5 .g4 .g3 facility power grid .g6
  # Minimum PowOut duration in seconds. [Default: 60]
  pow_out_min_rng: 60
  # Max PowOut duration in seconds. [Default: 180]
  pow_out_max_rng: 180
  # Min cooldown of PowOut events during match, in seconds. [Default: 360]
  pow_out_freq_min_rng: 360
  # Max cooldown of PowOut events during match, in seconds. [Default: 600]
  pow_out_freq_max_rng: 600
  # Should a power outage open all doors? [Default: True]
  pow_out_unlock_all_doors: true
```
